import React from "react";

import styled from 'styled-components';

const PageHeader = ({ title, horizonTitle }) => {

    return (
        <div>
            <HeaderTextStyle>{title}</HeaderTextStyle>
            <HorizonLineStyle>
                <SpanStyle>{ horizonTitle }</SpanStyle>
            </HorizonLineStyle>
        </div>
    );
};

export default PageHeader;

const HeaderTextStyle = styled.div`
    font-size: 36px;
    font-family: gyeonggi_title_bold;
`;

const HorizonLineStyle = styled.div`
    width: 100%;
    text-align: center;
    border-bottom: 1px solid #aaa;
    line-height: 0.1em;
    margin: 10px 0 20px;
`

const SpanStyle = styled.span`
    background: white;
    padding: 0 10px;
    font-size: 16px;
    font-family: gyeonggi_title_bold;
`;