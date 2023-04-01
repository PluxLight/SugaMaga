import styled from 'styled-components';

import React from 'react';

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import required modules
import { Autoplay, Pagination, Navigation } from 'swiper';

import img01 from './../../image/img01.png'
import img02 from './../../image/img02.jpeg'
import img03 from './../../image/img03.png'
import img04 from './../../image/img04.jpg'
import img05 from './../../image/img05.png'

function Home() {

  return (
    <HomePageStyle>
      <div>
        <InfoTextBox>
          <h2> &nbsp;최후의 1인이 되세요</h2>
          <h3> &nbsp;마녀의 손아귀에 끌려온 당신 <br />
          &nbsp;디저트로 둘러쌓인 곳에서 <br />
          &nbsp;과자로 된 무기를 들어 상대방을 무찌르고 <br />
          &nbsp;뻗쳐오는 마녀의 저주를 피해 <br />
          &nbsp;끝까지 살아남아 탈출하세요 <br /> 
          </h3>
        </InfoTextBox>
      <Swiper
        spaceBetween={30}
        centeredSlides={true}
        autoplay={{
          delay: 2000,
          disableOnInteraction: false,
        }}
        modules={[Autoplay, Pagination, Navigation]}
        className="mySwiper"
        style={{ width: '100%', height: '90vh', position: 'relative' }}
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
        <SwiperSlide>
          <ImageStyle src={img05} />
        </SwiperSlide>

        </Swiper>
      </div>
      
    </HomePageStyle>
  );
}

export default Home;

const HomePageStyle = styled.div`
  width: 100%;
  height: 100%;
`;

const InfoTextBox = styled.div`
  overflow: auto;
  width: 25%;
  height: 30%;
  z-index: 3;
  position: absolute;
  left: 100px;
  top: 40%;
  background: rgba(0, 0, 0, 0.6);
  color: white;
  &::-webkit-scrollbar {
    display: none;
  }
`;

const ImageStyle = styled.img`
  display: block;
  width: 100%;
  height: 100%;
  object-fit: cover;
`;
