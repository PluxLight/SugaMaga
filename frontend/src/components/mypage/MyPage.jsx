import React, { useEffect, useState } from "react";
import styled from 'styled-components';
import { useRecoilState } from 'recoil';
import { user, nickname } from "../../Store";

import PageHeader from "../../utils/PageHeader";
import ButtonMaker from "../../utils/ButtonMaker";
import { searchNickname, putUserNickname } from "../../api/sign";

import { updatePassword } from "firebase/auth";
import { firebaseAuth, onAuthStateChanged } from "../../firebase-config";


const MyPage = () => {
    const [recoilUser, setRecoilUser] = useRecoilState(user);
    const [recoilNickname, setRecoilNickname] = useRecoilState(nickname);
    const [inputNickname, setInputNickname] = useState("");
    const [msgNickname, setMsgNickname] = useState("　");
    const [nicknameOk, setNicknameOk] = useState(false);

    const [password, setPassword] = useState("");
    const [msgPassword, setMsgPassword] = useState("　");
    const [checkPassword, setCheckPassword] = useState("");
    const [msgCheckPassword, setMsgCheckPassword] = useState("　");
    const [passwordOk, setPasswordOk] = useState(false);
    
    const [errorMsg, setErrorMsg] = useState("　");
    
    const regexNickname = /^[A-Za-z0-9]{2,16}$/;
    const regexPassword =
        /^(?=.*[a-zA-Z])(?=.*[!@#$%^*+=-])(?=.*[0-9]).{6,25}$/;
    
    useEffect(() => {
        const debounce = setTimeout(() => {
            if (inputNickname) {
                if (regexNickname.test(inputNickname)) {
                    checkNickname();
                }
                else {
                    setMsgNickname(`닉네임 조건은 2~16자입니다.
                    영문과 숫자만을 허용합니다.`);
                    setNicknameOk(false);
                }
            }
        }, 200);
        return () => {
            clearTimeout(debounce);
        };
    }, [inputNickname]);

    useEffect(() => {
        onAuthStateChanged(firebaseAuth, fbUser => {
          const userCopy = JSON.parse(JSON.stringify(fbUser));
          setRecoilUser(userCopy);
        })
      }, [setRecoilUser])

    const nicknameChange = ({ target: { value } }) => {
        setInputNickname(value);
    }
    
    const checkNickname = () => {
        let param = {
          nickname: inputNickname
        };

        searchNickname(
            param,
            ({ data }) => {
                setMsgNickname("사용 가능한 닉네임입니다.");
                setNicknameOk(true);
            },
            (error) => {
                setMsgNickname("사용 불가능한 닉네임입니다.");
                setNicknameOk(false);
            }
        );
    };
    
    const passwordChange = ({ target: { value } }) => {
        setPassword(value);

        if (regexPassword.test(value)) {
            setMsgPassword(
                '사용가능한 비밀번호입니다.'
            );
        }
        else {
            setMsgPassword(
                '숫자+영문자+특수문자 조합으로 6자리 이상 입력해주세요.'
            );
        setPasswordOk(false);
        }

    }

    const checkPasswordChange = ({ target: { value } }) => {
        setCheckPassword(value);

        if (value !== password) {
            setMsgCheckPassword('비밀번호가 일치하지 않습니다.');
            setPasswordOk(false);
        }
        else {
            setMsgCheckPassword('비밀번호가 일치합니다.');
            setPasswordOk(true);
        }
    }

    const changeNicknameEvent = () => {
        if (!nicknameOk) {
            setErrorMsg('입력한 내용을 다시 확인해주세요');
            console.log('not allow change nickname');
            return;
        }

        let param = {
            nickname: inputNickname
        };
        let config = {
          headers: {
            uid: recoilUser.uid,
          }
        };
        
        putUserNickname(
            param, config,
            ({ data }) => {
                console.log(data);
                setMsgNickname("닉네임 변경에 성공했습니다.");
            },
            (error) => {
                console.log(error);
                setMsgNickname("닉네임 변경에 실패했습니다.");
            }
        );
    }

    const changePasswordEvent = () => {
        if (!passwordOk) {
            setErrorMsg('입력한 내용을 다시 확인해주세요');
            console.log('not allow change password');
            return;
        }

        updatePassword(firebaseAuth.currentUser, password)
            .then(() => {
                setErrorMsg('비밀번호 변경에 성공했습니다');
              })
            .catch((error) => {
                setErrorMsg('비밀번호 변경에 실패했습니다');
                console.log(error);
            })

    }
    
    return (
        <BackPage>
            <MyPageStyle>
                <PageHeader title="나의 정보" horizonTitle="Info" />
                <InfoDivStyle>
                    <InfoTextStyle>이메일</InfoTextStyle>
                    <InfoDetailTextStyle>{recoilUser.email}</InfoDetailTextStyle>
                </InfoDivStyle>

                <InfoDivStyle>
                    <InfoTextStyle>닉네임</InfoTextStyle>
                    <InputStyle
                            type="text"
                            name="nickname"
                            defaultValue={recoilNickname}
                            onChange={nicknameChange} />
                </InfoDivStyle>
                <RightBox>
                    <MsgStyle>{msgNickname}</MsgStyle>
                    <ButtonMaker value="닉네임 변경" event={changeNicknameEvent} text="닉네임 변경" />
                </RightBox>

                <InfoDivStyle>
                    <InfoTextStyle>비밀번호</InfoTextStyle>
                    <InputStyle
                            type="password"
                            name="password"
                            onChange={passwordChange} />
                </InfoDivStyle>
                <RightBox>
                    <MsgStyle>{msgPassword}</MsgStyle>
                </RightBox>

                <InfoDivStyle>
                    <InfoTextStyle>비밀번호 확인</InfoTextStyle>
                    <InputStyle
                            type="password"
                            name="checkPassword"
                            onChange={checkPasswordChange} />
                </InfoDivStyle>
                <RightBox>
                    <MsgStyle>{msgCheckPassword}</MsgStyle>
                    <ButtonMaker value="비밀번호 변경" event={changePasswordEvent} text="비밀번호 변경" />
                </RightBox>
                
                <MsgStyle>{errorMsg}</MsgStyle>
            </MyPageStyle>
        </BackPage>
        
    );
};

export default MyPage;

const BackPage = styled.div`
    width: 70%;
    height: 90vh%;
    justify-content: center;
    background-color: white;
`

const MyPageStyle = styled.div`
    width: 50%;
    margin-left: 25%;
    margin-top: 1%;
`;

const InfoDivStyle = styled.div`
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-top: 5%;
`;

const InfoTextStyle = styled.div`
    font-size: 28px;
    font-family: gyeonggi_title_bold;
`;

const InfoDetailTextStyle = styled.div`
    font-size: 22px;
    font-family: gyeonggi_title_bold;
`;

const InputStyle = styled.input`
    width: 60%;
    height: 30px;
    font-size: 18px;
    font-family: gyeonggi_title_bold;
    padding-left: 10px;
`;

const RightBox = styled.div`
    float: right;
    margin-top: 10px;
    margin-bottom: 10px;
`;

const MsgStyle = styled.div`
    display: inline-block;
    float: right;
    width: 100%;
    height: 30px;
    text-align: right;
    font-size: 18px;
    font-family: gyeonggi_title_bold;
`;

const ButtonStyle = styled.button`
    display: inline-block;
    float: right;
    width: 200px;
    height: 35px;
    align-items: center;
    justify-content: center;
    font-size: 22px;
    font-family: gyeonggi_title_bold;
    background-color: pink;
    border: none;
    cursor: pointer;
`;
