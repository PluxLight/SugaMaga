import './App.css';

import React, { useState, Component } from "react";
import { Routes, Route, useLocation, useNavigate } from "react-router-dom";
import styled from 'styled-components';
import Home from "./Home/Home";
import MyPage from "./MyPage/MyPage";
import Login from "./Sign/Login";

import Header from "./Nav/Header";

function App() {
  const router = useLocation();

  return (
    <>
      <MainHeader>
        <Header />
      </MainHeader>
      <div>
        {router.pathname === "/" ||
          router.pathname === "/mypage" ||
          router.pathname === "/login"
        }
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/mypage" element={<MyPage />}></Route>
          <Route path="/login" element={<Login />}></Route>
        </Routes>
      </div>
    </>
  );
}

export default App;

const MainHeader = styled.div`
  width: 100%;
  height: 60px;
  display: flex;
  align-items: center;
  flex-direction: row;
  justify-content: space-between;
  background: pink;
`;