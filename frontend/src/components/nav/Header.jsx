import { NavLink } from 'react-router-dom';
import styled from 'styled-components';

import { useRecoilValue, useRecoilState } from 'recoil';
import { user } from '../../Store';

const Header = () => {
  const LoginUser = useRecoilValue(user);

  const menus1 = [
      { name: 'Home', path: '/' },
      { name: '게임소개', path: '/introduce' },
      { name: '게임기록', path: '/history' },
      { name: '다운로드', path: '/download' }
  ];

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
  
  return (
    <HeaderArea>
      <Menu>{FirstMenu}</Menu>
      <Menu2>
        <Logout>
          로그아웃
        </Logout>
        {SecondMenu}</Menu2>
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

const Menu = styled.div`
  margin-left: 10px;
`;

const Menu2 = styled.div`
  margin-right: 10px;
  display: flex;
  align-items: center;
`;

const Logout = styled.div`
  margin-right: 10px;
  display: flex;
  align-items: center;
  color: black;
  font-size: 20px;
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

const NavBarStyle = styled(NavLink)`
    color: black;
    font-size: 20px;
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
