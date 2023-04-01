import styled from 'styled-components';

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import required modules
import { Autoplay, Pagination, Navigation } from 'swiper';

import img01 from './../../../image/img01.png'
import img02 from './../../../image/img02.jpeg'
import img03 from './../../../image/img03.png'
import img04 from './../../../image/img04.jpg'

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
            <Swiper
                modules={[Autoplay, Pagination, Navigation]}
                spaceBetween={30}
                centeredSlides={true}
                autoplay={{
                delay: 2000,
                disableOnInteraction: false,
                }}
                className="mySwiper"
                style={{ width: '80%', height: '30vh',  position: 'relative', border: '0.5px solid #aaa' }}
            >
                <SwiperSlide>
                <ImageStyle src={img01} />
                </SwiperSlide>
                <SwiperSlide>
                <ImageStyle src={img02} />
                </SwiperSlide>
                <SwiperSlide>
                <ImageStyle src={img03} />
                </SwiperSlide>
                <SwiperSlide>
                <ImageStyle src={img04} />
                </SwiperSlide>

            </Swiper>

            
            <TitleTextStyle>소개글</TitleTextStyle>
            <DetailTextStyle>
                {detailText}
            </DetailTextStyle>

        </IntroStyle>
    );
};

export default Intro;

const IntroStyle = styled.div`
`

const ImageStyle = styled.img`
    display: block;
    width: 100%;
    height: 100%;
    object-fit: cover;
`;


const TitleTextStyle = styled.div`
    font-size: 36px;
    font-family: gyeonggi_title_bold;
    margin-top: 50px;
`;

const DetailTextStyle = styled.div`
    white-space: pre-wrap;
    margin-top: 30px;
    font-size: 21px;
    font-family: gyeonggi_batang_regular;
    line-height: 1.5;
`