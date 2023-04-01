import styled from 'styled-components';

import { Link } from "react-router-dom";

const SideMenu = () => {

    return (
        <>
        <Link to="/introduce">intro | </Link>
        <Link to="/introduce/manual">manual | </Link>
        <Link to="/introduce/artwork">artwork</Link>
        </>
    );
};

export default SideMenu;