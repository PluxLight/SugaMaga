import styled from 'styled-components';
import React from "react";

import PageHeader from "../../utils/PageHeader";

function Error() {

  return (
    <ErrorStyle>
      <FormStyle>
        <PageHeader title="오류" horizonTitle="Error" />
        <TitleTextStyle>잘못된 경로 접근입니다.</TitleTextStyle>
        <InfoTextStyle>서비스를 계속 이용하시려면 홈페이지로 돌아가십시오.</InfoTextStyle>
      </FormStyle>
    </ErrorStyle>
  );
}

export default Error;

const ErrorStyle = styled.div`
    width: 50%;
    height: 80%;
    margin-top: 20px;
    background-color: white;
`

const FormStyle = styled.div`
    width: 70%;
    margin-top: 5%;
    margin-left: 15%;
`;

const TitleTextStyle = styled.div`
    font-size: 28px;
    font-family: gyeonggi_title_bold;
`;

const InfoTextStyle = styled.div`
    font-size: 22px;
    font-family: gyeonggi_title_bold;
    margin-top: 15px;
    margin-bottom: 15px;
`;