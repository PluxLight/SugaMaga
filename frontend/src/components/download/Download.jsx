import styled from 'styled-components';
import React, { useEffect, useState } from "react";

import PageHeader from "../../utils/PageHeader";
import TableMaker from "../../utils/TableMaker";

import img01 from './../../image/img01.png'

const Download = () => {
    const tableHeader = ['', '최소사양', '권장사양'];
    const tableRows = [
        {
            id: 'OS',
            columns: [
                {
                    key: 0,
                    render: 'OS'
                },
                {
                    key: 1,
                    render: 'Windows 10 64bit 이상'
                },
                {
                    key: 2,
                    render: 'Windows 10 64bit 이상'
                },
            ]
        },
        {
            id: 'CPU',
            columns: [
                {
                    key: 0,
                    render: 'CPU'
                },
                {
                    key: 1,
                    render: 'Intel i3, AMD Ryzen 3'
                },
                {
                    key: 2,
                    render: 'Intel i5, AMD Ryzen 5'
                },
            ]
        },
        {
            id: 'RAM',
            columns: [
                {
                    key: 0,
                    render: 'RAM'
                },
                {
                    key: 1,
                    render: '8GB'
                },
                {
                    key: 2,
                    render: '16GB'
                },
            ]
        },
        {
            id: 'GPU',
            columns: [
                {
                    key: 0,
                    render: 'GPU'
                },
                {
                    key: 1,
                    render: 'GTX 1050'
                },
                {
                    key: 2,
                    render: 'GTX 1060'
                },
            ]
        },
        {
            id: 'Direct X',
            columns: [
                {
                    key: 0,
                    render: 'Direct X'
                },
                {
                    key: 1,
                    render: 'Direct 11 이상'
                },
                {
                    key: 2,
                    render: 'Direct 11 이상'
                },
            ]
        },
        {
            id: 'Storage',
            columns: [
                {
                    key: 0,
                    render: '저장공간'
                },
                {
                    key: 1,
                    render: '30GB'
                },
                {
                    key: 2,
                    render: '30GB'
                },
            ]
        },
    ]

    return (
        <DownLoadStyle>
            <PageHeader title="다운로드" horizonTitle="Download" />
            <DownloadArea>
                <DownloadButton>
                    <LogoStyle src={img01} />
                    게임 다운로드</DownloadButton>
            </DownloadArea>

            <SpecArea>
                <TableMaker headers={ tableHeader } rows={ tableRows }></TableMaker>
            </SpecArea>

        </DownLoadStyle>
    );
};

export default Download;

const DownLoadStyle = styled.div`    
    width: 70%;
    height: 90vh%;
    justify-content: center;
    background-color: white;
    padding: 1%;
`;

const DownloadArea = styled.div`
    height: 10vh;
    margin-left: 30%;
    margin-right: 30%;
    margin-top: 5%;
`

const LogoStyle = styled.img`
    height: 100%;
`


const DownloadButton = styled.button`
    width: 100%;
    height: 100%;
    background-color: pink;
    border: none;
    font-size: 40px;
    font-family: gyeonggi_title_v_bold;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto;
`

const SpecArea = styled.div`
    width: 100%;
    height: 40vh;
    margin-top: 50px;
    display: flex;
    justify-content: center;
`
