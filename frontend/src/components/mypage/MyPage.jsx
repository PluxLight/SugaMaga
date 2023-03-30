import React, { useEffect, useState } from "react";
import styled from 'styled-components';
import { useRecoilState } from 'recoil';
import { user, nickname } from "../../Store";

import PageHeader from "../../utils/PageHeader";
import { getUserInfo, searchNickname, putUserNickname } from "../../api/sign";

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
    
    const regexNickname = /^[A-Za-z0-9가-힣]{2,16}$/;
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
                    공백과 특수문자를 허용하지 않습니다.`);
                    setNicknameOk(false);
                }
            }
        }, 200);
        return () => {
            clearTimeout(debounce);
        };
    }, [inputNickname]);
    
    useEffect(() => {
        // console.log(recoilUser.uid);
        let config = {
            headers: {
                uid: recoilUser.uid,
            }
        };
        getUserInfo(config,
            ({ data }) => {
                // console.log(data);
                setRecoilNickname(data);
            },
            (error) => {
                console.log(error);
            });
        
    }, []);

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
        <MyPageStyle>
            <PageHeader title="나의 정보" horizonTitle="Info" />
            <h2>이메일 : {recoilUser.email}</h2>
            
            <form>
                <label>
                    닉네임 : &nbsp;
                    <input
                        type="text"
                        name="nickname"
                        defaultValue={recoilNickname}
                        onChange={nicknameChange} />
                        <br />
                        {msgNickname}
                </label>
                <br />
                <input type="button" value="닉네임 변경" onClick={changeNicknameEvent} />
            </form>
            <form>
                <label>
                    비밀번호 : &nbsp;
                    <input
                    type="password"
                    name="password"
                    onChange={passwordChange} />
                    <br />
                    {msgPassword}
                </label>
                <br />
                <label>
                    비밀번호 확인 : &nbsp;
                    <input
                    type="password"
                    name="checkPassword"
                    onChange={checkPasswordChange} />
                    <br />
                    {msgCheckPassword}
                </label>
                <br />
                <input type="button" value="비밀번호 변경" onClick={changePasswordEvent} />
            </form>
            <h3>{errorMsg}</h3>
        </MyPageStyle>
    );
};

export default MyPage;

const MyPageStyle = styled.div`    
    width: 100vh;
    height: 100%;
    justify-content: center;
`;
