import { NavLink } from 'react-router-dom';
import styled from 'styled-components';

const Header = () => {
    const menus1 = [
        { name: 'Home', path: '/' },
        { name: '게임소개', path: '/introduce' },
        { name: '게임기록', path: '/history' },
        { name: '다운로드', path: '/download' }
    ];

    const menus2 = [
        { name: '로그인', path: '/login' },
        { name: '회원가입', path: '/signup' },
        { name: '나의 정보', path: '/mypage' }
    ];
    
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
            <Menu2>{SecondMenu}</Menu2>
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

`;

const Menu2 = styled.div`

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
