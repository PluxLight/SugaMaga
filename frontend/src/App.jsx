import './App.css';

import React, { useState, Component } from "react";
import { Routes, Route, useLocation, useNavigate } from "react-router-dom";
import styled from 'styled-components';
import Home from "./Home/Home";
import MyPage from "./MyPage/MyPage";
import Login from "./Sign/Login";
import SignUp from "./Sign/SignUp";
import History from "./History/History";
import Download from "./Download/Download";
import Introduce from "./Introduce/Introduce";

import Header from "./Nav/Header";

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
          router.pathname === "/signup"
        }
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/download" element={<Download />}></Route>
          <Route path="/history" element={<History />}></Route>
          <Route path="/introduce" element={<Introduce />}></Route>
          <Route path="/mypage" element={<MyPage />}></Route>
          <Route path="/login" element={<Login />}></Route>
          <Route path="/signup" element={<SignUp />}></Route>
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
  height: 85%;
  display: flex;
  justify-content: center;
`;