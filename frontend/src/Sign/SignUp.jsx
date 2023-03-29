import styled from 'styled-components';
import { useNavigate } from 'react-router-dom';


import React, { useState, useEffect } from "react";
import {
    firebaseAuth, createUserWithEmailAndPassword,
    sendEmailVerification
} from "./../firebase-config";
import { signup, searchNickname } from "./../api/sign";

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
  const [user, setUser] = useState();
  const [isAppropriate, setIsAppropriate] = useState();

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
        return;
      }

      const createdUser = await createUserWithEmailAndPassword(
        firebaseAuth, email, password)
        .then(() => {
          let user = firebaseAuth.currentUser;
          console.log("user uid : " + user.uid);

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

            })
            .catch("email send fail");
        });
      console.log('회원가입 성공!');
      emailChange("");
      passwordChange("");
      nicknameChange("");

    } catch (err) {
      //console.log(err.code);
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
          setErrorMsg('signup unknown error ...');
          break;
      }
    }
  }

  return (
    <SignUpForm>
      <h1>회원가입</h1>
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
              <br />
              {msgPassword}
          </label>
          <br />
          <label>
            check password:
            <input
              type="password"
              name="checkPassword"
              onChange={checkPasswordChange} />
            <br />
            {msgCheckPassword}
          </label>
          <br />
          <label>
            nickname:
            <input
              type="text"
              name="nickname"
              onChange={nicknameChange} />
            <br />
            {msgNickname}
          </label>
          <br />
          <input type="button" value="SignUp" onClick={signUpEvent} />
        </form>
        <h3>{errorMsg}</h3>
      </div>
    </SignUpForm>
  );
}

export default SignUp;


const SignUpForm = styled.div`

`;