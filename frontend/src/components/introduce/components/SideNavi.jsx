import styled from 'styled-components';
import { NavLink } from 'react-router-dom';

const SideMenu = () => {
    const menus = [
        { name: '- 설명', path: '/introduce/intro' },
        { name: '- 조작법', path: '/introduce/manual' },
        { name: '- 아트워크', path: '/introduce/artwork' }
    ];

    const sideMenu = menus.map((menu, index) => {
        return (
          <LinkStyle
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
          </LinkStyle>
        );
    });

    return (
        <NaviBox>

            <HeaderStyle>게임소개</HeaderStyle>
            <HorizonLineStyle />

            {sideMenu}

        </NaviBox>
    );
};

export default SideMenu;

const NaviBox = styled.div`
    background: pink;
    width: 10vw;
    height: 25vh;
    margin: 15% auto;
    overflow: hidden;
`

const HeaderStyle = styled.div`
    font-family: gyeonggi_title_bold;
    font-size: 26px;
    margin: 7px;
    marginLeft: 10px;
    marginRight: 2px;
    marginTop: 10px;
    padding-top: 10px;
`

const HorizonLineStyle = styled.div`
    width: 90%;
    text-align: center;
    border-bottom: 1px solid #aaa;
    border-color: hotpink;
    line-height: 0.1em;
    margin: 10px 5px 20px 5px;
    display: flex;
`

const LinkStyle = styled(NavLink)`
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
    display: block;
`