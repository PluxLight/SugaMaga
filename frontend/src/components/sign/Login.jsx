import styled from 'styled-components';

import { useNavigate } from 'react-router-dom';

import React, { useEffect, useState } from "react";
import { useRecoilState } from 'recoil';
import { user, nickname } from "../../Store";

import {
  firebaseAuth, signInWithEmailAndPassword,
  onAuthStateChanged
} from "../../firebase-config";

import PageHeader from '../../utils/PageHeader';
import ButtonMaker from "../../utils/ButtonMaker";
import { getUserInfo } from "../../api/sign";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("　");
  const [isAppropriate, setIsAppropriate] = useState();

  const [login, setLogin] = useRecoilState(user);
  
  const [recoilNickname, setRecoilNickname] = useRecoilState(nickname);

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

      let config = {
        headers: {
            uid: curUserInfo.user.uid,
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
        <ButtonMaker value="Login" event={logInEvent} text="로그인" />
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

    border-radius: 5px;
    padding: 10px 25px;
    font-weight: 500;
    background: transparent;
    cursor: pointer;
    transition: all 0.3s ease;
    position: relative;
    display: inline-block;
    box-shadow:inset 2px 2px 2px 0px rgba(255,255,255,.5),
    7px 7px 20px 0px rgba(0,0,0,.1),
    4px 4px 5px 0px rgba(0,0,0,.1);
    outline: none;

    background-color: #89d8d3;
    background-image: linear-gradient(315deg, #89d8d3 0%, #03c8a8 74%);
    border: none;
    z-index: 1;
    
    &:after {
      position: absolute;
      content: "";
      width: 100%;
      height: 0;
      bottom: 0;
      left: 0;
      z-index: -1;
      border-radius: 5px;
      background-color: #4dccc6;
      background-image: linear-gradient(315deg, #4dccc6 0%, #96e4df 74%);
      box-shadow:
      -7px -7px 20px 0px #fff9,
      -4px -4px 5px 0px #fff9,
      7px 7px 20px 0px #0002,
      4px 4px 5px 0px #0001;
      transition: all 0.3s ease;
    }

    &:hover {
      color: #fff;
    }

    &:hover:after {
      top: 0;
      height: 100%;
    }

    &.active {
      top: 2px;
    }
`;

/*
.btn-13 {
  background-color: #89d8d3;
  background-image: linear-gradient(315deg, #89d8d3 0%, #03c8a8 74%);
  border: none;
  z-index: 1;
}
.btn-13:after {
  position: absolute;
  content: "";
  width: 100%;
  height: 0;
  bottom: 0;
  left: 0;
  z-index: -1;
  border-radius: 5px;
   background-color: #4dccc6;
  background-image: linear-gradient(315deg, #4dccc6 0%, #96e4df 74%);
  box-shadow:
   -7px -7px 20px 0px #fff9,
   -4px -4px 5px 0px #fff9,
   7px 7px 20px 0px #0002,
   4px 4px 5px 0px #0001;
  transition: all 0.3s ease;
}
.btn-13:hover {
  color: #fff;
}
.btn-13:hover:after {
  top: 0;
  height: 100%;
}
.btn-13:active {
  top: 2px;
}
*/

/*
    background-color: pink;
    background-image: linear-gradient(315deg, pink 0%, HotPink 74%);
*/