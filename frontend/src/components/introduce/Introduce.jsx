import styled from 'styled-components';

import { Routes, Route, useLocation } from "react-router-dom";

import PageHeader from "../../utils/PageHeader";
import Intro from "./components/Intro";
import Manual from "./components/Manual";
import Artwork from "./components/Artwork";
import SideNavi from "./components/SideNavi";
import Error from "../error/Error";

const Introduce = () => {

    return (
        <IntroStyle>

            <ContensBox>
                <SideNaviBox>
                    <SideNavi></SideNavi>
                </SideNaviBox>

                <DetailBox>
                    <PageHeader title="게임소개" horizonTitle="Introduce" />
                    <Routes>
                        <Route path="intro" element={<Intro />}></Route>
                        <Route path="manual" element={<Manual />}></Route>
                        <Route path="artwork" element={<Artwork />}></Route>
                        <Route path="/*" element={<Error />}></Route>
                    </Routes>
                </DetailBox>

            </ContensBox>

        </IntroStyle>
    );
};

export default Introduce;

const IntroStyle = styled.div`
    width: 70%;
    height: 90vh%;
    justify-content: center;
    background-color: white;
    padding: 1%;
`;

const ContensBox = styled.div`
    display: flex;
    width: 100%;
    height: 100%;
`;

const SideNaviBox = styled.div`
    width: 20%;
`;

const DetailBox = styled.div`
    width: 70%;
    height: 100%;
`;