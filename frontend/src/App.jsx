import './App.css';

import styled from 'styled-components';
import React, { useState } from "react";

import {
  firebaseAuth, createUserWithEmailAndPassword,
  signInWithEmailAndPassword, sendEmailVerification,
  EmailAuthProvider
} from "./firebase-config";
import axios from "axios";

function App() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("　");
  const [user, setUser] = useState();
  const [isAppropriate, setIsAppropriate] = useState();

  const emailChange = ({ target: { value } }) => {
    setEmail(value);
  }

  const passwordChange = ({ target: { value } }) => {
    setPassword(value);
  }

  const signUpEvent = async (event) => {
    event.preventDefault();

    try {
      setErrorMsg('　');
      const createdUser = await createUserWithEmailAndPassword(
        firebaseAuth, email, password)
        .then(() => {
          let user = firebaseAuth.currentUser;

          sendEmailVerification(user)
            .then(function () {
              console.log('email send success');
            })
            .catch("email send fail");
        });
      console.log(createdUser);
      console.log('회원가입 성공!');
      setEmail("");
      setPassword("");
  
    } catch(err){
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

  const logInEvent = async (event) => {
    event.preventDefault();

    try {
      const curUserInfo = await signInWithEmailAndPassword(firebaseAuth, email, password);
      console.log(curUserInfo);
      setUser(curUserInfo.user);
      setErrorMsg(" ");

    } catch(err){
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

  const verifyAuth = () => {
    // For an email/password user. Prompt the user for the password again.
    axios({
      method: 'POST',
      url: `https://securetoken.googleapis.com/v1/token?key=` + process.env.REACT_APP_FB_API_KEY,
      headers: {
        "Content-Type": `application/x-www-form-urlencoded`,
      },
      data: {
        'grant_type': 'refresh_token',
        'refresh_token' : 'APJWN8cTeMA1F480VhgW5WQ3LwIzny_n1cbQ1VhSChS8KLRc9Gpi0yu-l1j3K41JULJuci8wwRrMOGlRmmVrWd3UIFT-8aeisY8HWaXcfmusXjhTj8uovrb7i3AKTN4eAwp0jcPZpjRoqzGRKM0E8U8hRLYwdxJOB-EzLX2jPgVhbAst511jo422QICtHOyvc9LTEqgQUUSnXCmm5lKWLo4wj0otJVj8AA'
      }
    }).then((response) => {
      console.log(response);
    })
  }

  const fileDownload = (event) => {
    console.log("click");

    axios({
      method: 'GET',
      url: `http://localhost:8080/api/file/download`,
      responseType: "blob",
    }).then((response) => {
      console.log(response);

      // 다운로드(서버에서 전달 받은 데이터) 받은 바이너리 데이터를 blob으로 변환합니다.
      const blob = new Blob([response.data]);
      // 특정 타입을 정의해야 경우에는 옵션을 사용해 MIME 유형을 정의 할 수 있습니다.
      // const blob = new Blob([this.content], {type: 'text/plain'})

      // blob을 사용해 객체 URL을 생성합니다.
      const fileObjectUrl = window.URL.createObjectURL(blob);

      // blob 객체 URL을 설정할 링크를 만듭니다.
      const link = document.createElement("a");
      link.href = fileObjectUrl;
      link.style.display = "none";
      
      // 다운로드 파일 이름을 추출하는 함수
      const extractDownloadFilename = (response) => {
          const disposition = response.headers["content-disposition"];
          const fileName = decodeURI(
          disposition
              .match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/)[1]
              .replace(/['"]/g, "")
          );
          return fileName;
      };

      // 다운로드 파일 이름을 지정 할 수 있습니다.
      // 일반적으로 서버에서 전달해준 파일 이름은 응답 Header의 Content-Disposition에 설정됩니다.
      link.download = extractDownloadFilename(response);


      // 다운로드 파일의 이름은 직접 지정 할 수 있습니다.
      // link.download = "sample-file.txt";

      // 링크를 body에 추가하고 강제로 click 이벤트를 발생시켜 파일 다운로드를 실행시킵니다.
      document.body.appendChild(link);
      link.click();
      link.remove();

      // 다운로드가 끝난 리소스(객체 URL)를 해제합니다.
      window.URL.revokeObjectURL(fileObjectUrl);
    })
  }

  return (
    <div className="App">
      <h1>Tayoverse</h1>
      <div>
        <form>
          <label>
            email:
            <input
              type="email"
              name="email"
              onChange={emailChange}/>
          </label>
          <br/>
          <label>
            password:
            <input
              type="password"
              name="password"
              onChange={passwordChange}/>
          </label>
          <br/>
          <input type="button" value="SignUp" onClick={signUpEvent} />
          &nbsp;
          <input type="button" value="Login" onClick={logInEvent} />
        </form>
        <h3>{ errorMsg }</h3>
      </div>
      <button onClick={verifyAuth}>token re</button>
      <br/>
      <div>
        <FileDown onClick={ fileDownload }>Tayoverse DownLoad</FileDown>
      </div>
      <a href="http://localhost:8080/api/file/download">파일 다운로드</a>
    </div>
  );
}

export default App;

const Login = styled.button`
  height: 40px;
  border-radius: 12px;
`;

const FileDown = styled.button`
  height: 40px;
  border-radius: 12px;
`;
