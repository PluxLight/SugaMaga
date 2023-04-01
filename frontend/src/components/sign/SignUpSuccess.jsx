import styled from 'styled-components';
import React from "react";

import PageHeader from "../../utils/PageHeader";

function SignUpSuccess() {

  return (
    <SignUpSuccessStyle>
      <FormStyle>
        <PageHeader title="회원가입 성공" horizonTitle="SignUp Success" />
        <TitleTextStyle>회원가입시 입력한 이메일로 확인 메일을 발송했습니다.</TitleTextStyle>
        <InfoTextStyle>이메일 인증 완료 후 서비스 이용이 가능합니다.</InfoTextStyle>
        <InfoTextStyle>이메일에 안내된 절차대로 진행해주시기 바랍니다.</InfoTextStyle>
      </FormStyle>
    </SignUpSuccessStyle>
  );
}

export default SignUpSuccess;

const SignUpSuccessStyle = styled.div`
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