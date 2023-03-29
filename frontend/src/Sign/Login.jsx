import styled from 'styled-components';
import { useNavigate } from 'react-router-dom';

import React, { useState } from "react";
import {
    firebaseAuth, signInWithEmailAndPassword
} from "./../firebase-config";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("　");
  const [user, setUser] = useState();
  const [isAppropriate, setIsAppropriate] = useState();

  const navigate = useNavigate();

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
      setUser(curUserInfo.user);
      setErrorMsg(" ");
      navigate('/')
      

    } catch (err) {
      setIsAppropriate(false);
      // console.log(err.code);
      switch (err.code) {
        case 'auth/user-not-found.':
          setErrorMsg('존재하지 않는 유저입니다');
          break;
        case 'auth/wrong-password':
          setErrorMsg('잘못된 비밀번호입니다');
          break;
        default:
          setErrorMsg('signin unknown error ...');
          break;
      }
    }
  }

  return (
    <div className="Login">
      <h1>Login</h1>
      <div>
        <form>
          <label>
            email:
            <input
              type="email"
              name="email"
              onChange={emailChange} />
          </label>
          <br />
          <label>
            password:
            <input
              type="password"
              name="password"
              onChange={passwordChange} />
          </label>
          <br />
          <input type="button" value="Login" onClick={logInEvent} />
        </form>
        <h3>{errorMsg}</h3>
      </div>
      <br />
    </div>
  );
}

export default Login;