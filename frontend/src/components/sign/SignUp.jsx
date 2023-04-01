import styled from 'styled-components';
import { useNavigate } from 'react-router-dom';


import React, { useState, useEffect } from "react";
import {
    firebaseAuth, createUserWithEmailAndPassword,
    sendEmailVerification, signOut
} from "../../firebase-config";

import { signup, searchNickname } from "../../api/sign";
import PageHeader from "../../utils/PageHeader";

function SignUp() {
  const [email, setEmail] = useState("");

  const [password, setPassword] = useState("");
  const [msgPassword, setMsgPassword] = useState("　");
  const [checkPassword, setCheckPassword] = useState("");
  const [msgCheckPassword, setMsgCheckPassword] = useState("　");
  const [passwordOk, setPasswordOk] = useState(false);

  const [nickname, setNickname] = useState("");
  const [msgNickname, setMsgNickname] = useState("　");
  const [nicknameOk, setNicknameOk] = useState(false);
  
  const [errorMsg, setErrorMsg] = useState("　");

  const regexNickname = /^[A-Za-z0-9가-힣]{2,16}$/;
  const regexPassword =
      /^(?=.*[a-zA-Z])(?=.*[!@#$%^*+=-])(?=.*[0-9]).{6,25}$/;

  useEffect(() => {
    const debounce = setTimeout(() => {
      if (nickname) {
        if (regexNickname.test(nickname)) {
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
  }, [nickname]);

  const navigate = useNavigate();

  const emailChange = ({ target: { value } }) => {
    setEmail(value);
  }

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

  const nicknameChange = ({ target: { value } }) => {
    setNickname(value);
  }

  const checkNickname = () => {
    let param = {
      nickname: nickname
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

  const signUpEvent = async (event) => {
    event.preventDefault();

    try {
      setErrorMsg('　');
      if (!nicknameOk || !passwordOk) {
        setErrorMsg('입력한 내용을 다시 확인해주세요');
        console.log('not allow signup');
        return;
      }
      
      // 가입 -> 유저정보 받아오기 -> 이메일 송신 -> 유저정보 변수저장 ->
      // DB에 유저정보 저장 -> 로그아웃해서 해더 로그인 상태해제
      const createdUser = await createUserWithEmailAndPassword(
        firebaseAuth, email, password)
        .then(() => {
          let user = firebaseAuth.currentUser;

          sendEmailVerification(user)
            .then(function () {
              console.log('email send success');
              
              let param = {
                email: email,
                nickname: nickname
              };
              let config = {
                headers: {
                  uid: user.uid,
                }
              };

              signup(
                param, config,
                ({ data }) => {
                  console.log(data.data);
                  navigate('/signupsuccess')
                },
                (error) => {
                  console.log(error);
                });
              
              signOut(firebaseAuth)
                .then(() => {
                  console.log("logout");
                })
                .catch(e => {
                  console.log("fail : " + e);
                });
            })
            .catch("email send fail");
        });
      console.log('회원가입 성공!');

    } catch (err) {
      
      switch (err.code) {
        case 'auth/weak-password':
          setErrorMsg('비밀번호는 6자리 이상이어야 합니다');
          break;
        case 'auth/invalid-email':
          setErrorMsg('잘못된 이메일 주소입니다');
          break;
        case 'auth/email-already-in-use':
          setErrorMsg('이미 가입되어 있는 계정입니다');
          break;
        default:
          console.log('signup unknown error ...');
          break;
      }
    }
  }

  return (
    <SignUpStyle>
      <FormStyle>
        <PageHeader title="회원가입" horizonTitle="SignUp" />
        <InfoDivStyle>
          <InfoTextStyle>이메일</InfoTextStyle>
          <InputStyle
            type="email"
            name="email"
            onChange={emailChange} />
        </InfoDivStyle>

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
        </RightBox>
        
        <InfoDivStyle>
            <InfoTextStyle>닉네임</InfoTextStyle>
            <InputStyle
                    type="text"
                    name="nickname"
                    onChange={nicknameChange} />
        </InfoDivStyle>
        <RightBox>
          <MsgStyle>{msgNickname}</MsgStyle>
          <ButtonStyle value="회원가입" onClick={signUpEvent} >회원가입</ButtonStyle>
          <MsgStyle>{errorMsg}</MsgStyle>
        </RightBox>

      </FormStyle>
    </SignUpStyle>
    
  );
}

export default SignUp;

const SignUpStyle = styled.div`
    width: 50%;
    height: 80%;
    margin-top: 20px;
    background-color: white;
`

const FormStyle = styled.div`
    width: 70%;
    margin-top: 5%;
    margin-left: 15%;
`;


const InfoDivStyle = styled.div`
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-top: 5%;
`;

const InfoTextStyle = styled.div`
    font-size: 24px;
    font-family: gyeonggi_bold;
`;

const InputStyle = styled.input`
    width: 60%;
    height: 30px;
    font-size: 18px;
    font-family: gyeonggi_bold;
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
    font-family: gyeonggi_bold;
`;

const ButtonStyle = styled.button`
    display: inline-block;
    float: right;
    width: 150px;
    height: 28px;
    align-items: center;
    justify-content: center;
    font-size: 22px;
    font-family: gyeonggi_bold;
    margin-bottom: 15px;
`;
