import './App.css';

import React from "react";
import { Routes, Route, useLocation } from "react-router-dom";
import styled from 'styled-components';

import Header from "./components/nav/Header";

import Home from "./components/home/Home";
import Download from "./components/download/Download";
import History from "./components/history/History";
import Introduce from "./components/introduce/Introduce";
import MyPage from "./components/mypage/MyPage";
import Login from "./components/sign/Login";
import SignUp from "./components/sign/SignUp";
import SignUpSuccess from "./components/sign/SignUpSuccess"; 

function App() {
  const router = useLocation();

  return (
    <>
      <MainHeader>
        <Header />
      </MainHeader>
      <MainArea>
        {router.pathname === "/" ||
          router.pathname === "/download" ||
          router.pathname === "/history" ||
          router.pathname === "/introduce" ||
          router.pathname === "/mypage" ||
          router.pathname === "/login" ||
          router.pathname === "/signup" ||
          router.pathname === "/signupsuccess"
        }
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/download" element={<Download />}></Route>
          <Route path="/history" element={<History />}></Route>
          <Route path="/introduce" element={<Introduce />}></Route>
          <Route path="/mypage" element={<MyPage />}></Route>
          <Route path="/login" element={<Login />}></Route>
          <Route path="/signup" element={<SignUp />}></Route>
          <Route path="/signupsuccess" element={<SignUpSuccess />}></Route>
        </Routes>
      </MainArea>
    </>
  );
}

export default App;

const MainHeader = styled.div`
  width: 100%;
  height: 60px;
`;

const MainArea = styled.div`
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
`;