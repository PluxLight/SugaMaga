import styled from 'styled-components';

import { useNavigate } from 'react-router-dom';

import React, { useEffect, useState } from "react";
import { useRecoilState } from 'recoil';
import { user } from "../../Store";

import {
  firebaseAuth, signInWithEmailAndPassword,
  onAuthStateChanged
} from "../../firebase-config";

import PageHeader from '../../utils/PageHeader';

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("　");
  const [isAppropriate, setIsAppropriate] = useState();

  const [login, setLogin] = useRecoilState(user);

  const navigate = useNavigate();

  useEffect(() => {
    onAuthStateChanged(firebaseAuth, fbUser => {
      const userCopy = JSON.parse(JSON.stringify(fbUser));
      setLogin(userCopy);
    })
  }, [setLogin])

  const emailChange = ({ target: { value } }) => {
    setEmail(value);
  }

  const passwordChange = ({ target: { value } }) => {
    setPassword(value);
  }

  const logInEvent = async (event) => {
    event.preventDefault();

    try {
      const curUserInfo = await signInWithEmailAndPassword(firebaseAuth, email, password);
      // if (!curUserInfo.user.emailVerified) {
      //   setErrorMsg('이메일 인증이 완료되지 않았습니다.');
      //   return;
      // }
      
      console.log(curUserInfo);
      // setLogin(curUserInfo.user);
      setErrorMsg(" ");
      navigate('/')
    } catch (err) {
      console.log(err.code);
      setIsAppropriate(false);
      switch (err.code) {
        case 'auth/user-not-found':
          setErrorMsg('존재하지 않는 유저입니다');
          break;
        case 'auth/wrong-password':
          setErrorMsg('잘못된 비밀번호입니다');
          break;
        default:
          console.log('signin unknown error ...');
          break;
      }
    }
  }

  const activeEnter = (e) => {
    if (e.key === "Enter") {
      logInEvent(e);
    }
  }

  return (
    <LoginStyle>
      <FormStyle>
        <PageHeader title="로그인" horizonTitle="Login" />
        <InputDivStyle>
          <InputTextStyle>이메일</InputTextStyle>
          <InputStyle
                type="email"
                name="email"
                onChange={emailChange} />
        </InputDivStyle>
        <InputDivStyle>
          <InputTextStyle>비밀번호</InputTextStyle>
          <InputStyle
                type="password"
                name="password"
                onChange={passwordChange} 
                onKeyDown={activeEnter}/>
        </InputDivStyle>
        <LoginButton  value="Login" onClick={logInEvent} >
          로그인
          </LoginButton>
      </FormStyle>
      <div>        
        <h3>{errorMsg}</h3>
      </div>
      <br />
    </LoginStyle>
  );
}

export default Login;


const LoginStyle = styled.div`    
    width: 50%;
    height: 80%;
    margin-top: 20px;
    background-color: white;
`;

const FormStyle = styled.div`
    width: 70%;
    margin-top: 5%;
    margin-left: 15%;
`;

const InputDivStyle = styled.div`
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-top: 5%;
`;

const InputTextStyle = styled.div`
    font-size: 24px;
    font-family: gyeonggi_title_bold;
`;

const InputStyle = styled.input`
    width: 60%;
    height: 30px;
    font-size: 18px;
    font-family: gyeonggi_title_bold;
    padding-left: 10px;
`;

const LoginButton = styled.button`
    display: flex;
    float: right;
    width: 35%;
    height: 40px;
    margin-top: 5%;
    align-items: center;
    justify-content: center;
    font-size: 22px;
    font-family: gyeonggi_title_bold;
    background-color: pink;
    border: none;
    cursor: pointer;
`;