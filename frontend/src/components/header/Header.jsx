import { NavLink, useNavigate } from 'react-router-dom';
import styled from 'styled-components';

import { useRecoilValue, useRecoilState } from 'recoil';
import { user, nickname } from '../../Store';
import React, { useEffect } from "react";

import {
  firebaseAuth, onAuthStateChanged,
  signOut
} from "../../firebase-config";

import img01 from './../../image/img01.png'

const Header = () => {
  const [login, setLogin] = useRecoilState(user);
  const [recoilUser, setRecoilUser] = useRecoilState(user);
  const [recoilNickname, setRecoilNickname] = useRecoilState(nickname);
  
  const navigate = useNavigate();

  useEffect(() => {
    onAuthStateChanged(firebaseAuth, fbUser => {
      const userCopy = JSON.parse(JSON.stringify(fbUser));
      setLogin(userCopy);
    })
  }, [setLogin])
  
  const LoginUser = useRecoilValue(user);

  const menus1 = 
    (LoginUser == null ?
      [{ name: '게임소개', path: '/introduce/intro' },
        { name: '다운로드', path: '/download' }]
      : [{ name: '게임소개', path: '/introduce/intro' },
      { name: '게임기록', path: '/history' },
        { name: '다운로드', path: '/download' }]
    );
      

  const menus2 =
    (LoginUser == null ?
      [{ name: '로그인', path: '/login' },
        { name: '회원가입', path: '/signup' }]
      : [{ name: '나의 정보', path: '/mypage' }]
    );
  
  const FirstMenu = menus1.map((menu, index) => {
      return (
        <NavBarStyle
          exact="true"
          style={{
            textDecoration: 'none',
            margin: '7px',
            marginLeft: '10px',
            marginRight: '2px',
          }}
          to={menu.path}
          key={index}
          >
              {menu.name}
        </NavBarStyle>
      );
  });

  const ToHomePage = () => {
    navigate('/');
  }

  const SecondMenu = menus2.map((menu, index) => {
      return (
        <NavBarStyle
          exact="true"
          style={{
            textDecoration: 'none',
            margin: '7px',
            marginLeft: '10px',
            marginRight: '2px',
            marginTop: '10px',
          }}
          to={menu.path}
          key={index}
        >
          {menu.name}
        </NavBarStyle>
      );
  });
  
  const LogOutEvent = async () => {
    navigate("/")
    
    const curUserInfo = await signOut(firebaseAuth)
      .then(() => {
        console.log("logout");
        setRecoilNickname("");
      })
      .catch(e => {
        console.log("fail : " + e);
      });
  }
  
  return (
    <HeaderArea>
      <Menu>
        <LogoStyle src={img01} onClick={ToHomePage} />
        {FirstMenu}
      </Menu>
      <Menu2>
        {LoginUser != null ?
          <LogoutLink to={'/login'}
            onClick={LogOutEvent}
            size="24"
            style={{ margin: '10px', marginLeft: '10px', cursor: 'pointer' }}>
            로그아웃
          </LogoutLink>
        : <></>}
        {SecondMenu}
      </Menu2>
    </HeaderArea>
  );

};

export default Header;

const HeaderArea = styled.div`
    height: 100%;
    display: flex;
    background: pink;
    align-items: center;
    justify-content: space-between;
`;

const LogoStyle = styled.img`
  weight: 5wh;
  height: 9vh;
  cursor: pointer;
`

const Menu = styled.div`
  margin-left: 10px;
  display: flex;
  align-items: center;
`;

const Menu2 = styled.div`
  margin-right: 10px;
  display: flex;
  align-items: center;
`;

const LogoutLink = styled.div`
  margin-right: 10px;
  display: flex;
  align-items: center;
  color: black;
  font-size: 24px;
  font-family: gyeonggi_title_bold;
  outline: invert;
  &:hover {
    transition: 0.5s;
    color: white;
  }
  &.active {
  color: HotPink;
  }
`;

const NavBarStyle = styled(NavLink)`
    color: black;
    font-family: gyeonggi_title_bold;
    font-size: 24px;
    outline: invert;
    &:link {
    transition: 0.5s;
    text-decoration: none;
    }
    &:hover {
    color: white;
    }
    &.active {
    color: HotPink;
    }
`;
