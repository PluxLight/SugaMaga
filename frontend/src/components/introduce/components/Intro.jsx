import styled from 'styled-components';

const Intro = () => {

    const detailText =
        "옛날 옛적에, 사람을 과자로 만들어 잡아먹는 마녀가 살았습니다.\n"
        + "근데 그 마녀가 여러분의 뒷덜미를 잡아채서 끌고갔어요!\n\n"
    
        + "그런데 모으고 보니 사람이 한명... 두명... 오십명... 너무 많은데요?\n"
        + "마녀는 갑자기 요리하기 귀찮아졌습니다. 이제 나이가 들어서 대량 조리는 힘들대요.\n"
        + "그때, 마녀의 머리에 좋은 아이디어가 생각났습니다. \n"
        + "\'!\'\n"
        + "'자기들끼리 싸워서 과자로 만들게 두면 되잖아?'\n"
        + "마녀는 마지막에 살아남은 한 명만 살려서 보내주기로 하고\n"
        + "외딴 과자섬으로 사람들을 보내버렸습니다.\n\n"
        
        + "열심히 사람들을 쿠키 반죽으로 만들고 최후의 1인이 되어 탈출하세요!\n";
        

    return (
        <IntroStyle>
            <TitleTextStyle>설명</TitleTextStyle>
            <ImageStyle src="https://sugamaga.aeoragy.com/api/file/images/logo.png" />

            
            <TitleTextStyle2>소개글</TitleTextStyle2>
            <DetailTextStyle>
                {detailText}
            </DetailTextStyle>

        </IntroStyle>
    );
};

export default Intro;

const IntroStyle = styled.div`
    height: 70vh;

    overflow: scroll;
    &::-webkit-scrollbar {
      display: none;
    }
`

const ImageStyle = styled.img`
    display: block;
    width: 80%;
    object-fit: cover;
    margin: 0 auto;
`;

const TitleTextStyle = styled.div`
    font-size: 2.2vw;
    font-family: gyeonggi_title_medium;
    margin-top: 1vh;
    margin-bottom: 15px;
`;

const TitleTextStyle2 = styled.div`
    font-size: 1.8vw;
    font-family: gyeonggi_title_medium;
    margin-top: 2vh;
`;

const DetailTextStyle = styled.div`
    height: 50%;
    white-space: pre-wrap;
    margin-top: 1.5vh;
    font-size: 1.3vw;
    font-family: gyeonggi_batang_regular;
    line-height: 1.5;
`