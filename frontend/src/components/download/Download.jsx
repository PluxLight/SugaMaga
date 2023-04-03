import styled from 'styled-components';
import React from "react";

import PageHeader from "../../utils/PageHeader";
import TableMaker from "../../utils/TableMaker";

import axios from "axios";

import logo from './../../image/img01.png'

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

    const downloadEvent = () => {    
        axios({
            method: 'GET',
            url: `https://aeoragy.com/api/file/download?filename=launcher.exe`,
            responseType: "blob",
        }).then((response) => {
            // console.log(response);
    
            // 다운로드(서버에서 전달 받은 데이터) 받은 바이너리 데이터를 blob으로 변환
            const blob = new Blob([response.data]);
    
            // blob을 사용해 객체 URL을 생성
            const fileObjectUrl = window.URL.createObjectURL(blob);
    
            // blob 객체 URL을 설정할 링크 생성
            const link = document.createElement("a");
            link.href = fileObjectUrl;
            link.style.display = "none";
            
            // 다운로드 파일 이름을 추출하는 함수
            const extractDownloadFilename = (response) => {
                const disposition = response.headers["content-disposition"];
                const fileName = decodeURI(
                disposition
                    .match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/)[1]
                    .replace(/['"]/g, "")
                );
                return fileName;
            };
    
            // 다운로드 파일 이름 지정
            link.download = extractDownloadFilename(response);
    
            // 링크를 body에 추가하고 강제로 click 이벤트를 발생시켜 파일 다운로드를 실행
            document.body.appendChild(link);
            link.click();
            link.remove();
    
            // 다운로드가 끝난 리소스(객체 URL)를 해제합니다.
            window.URL.revokeObjectURL(fileObjectUrl);
        })
    }
    

    return (
        <DownLoadStyle>
            <PageHeader title="다운로드" horizonTitle="Download" />
            <DownloadArea>
                <DownloadButton onClick={ downloadEvent } >
                    <LogoStyle src={logo} />
                    게임 다운로드
                </DownloadButton>
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
    font-size: 2.5vw;
    font-family: gyeonggi_title_v_bold;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto;
    overflow: hidden;
`

const SpecArea = styled.div`
    width: 100%;
    height: 40%;
    margin-top: 4vh;
    display: flex;
    justify-content: center;
`
