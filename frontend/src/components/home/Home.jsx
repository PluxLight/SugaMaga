import styled from 'styled-components';
import React, { useEffect, useState } from "react";

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import required modules
import { Autoplay, Pagination, Navigation } from 'swiper';

import { getImageList } from '../../api/file'

function Home() {
  const [images, setImages] = useState([]);

  const titleText = "최후의 1인이 되세요";
  const detailText = "마녀의 손아귀에 끌려온 당신\n"
    + "디저트로 둘러쌓인 곳에서\n"
    + "과자로 된 무기를 들어 상대방을 무찌르고\n"
    + "뻗쳐오는 마녀의 저주를 피해\n"
    + "끝까지 살아남아 탈출하세요\n";
  
  const param = "home";
  
  useEffect(() => {
    let params = {
      imagesKey: param
    };

    getImageList(params,
      ({ data }) => {
        setImages(data);
      },
      (error) => {
        console.log(error);
        }
    )
    }, [])

  const slideImages = images.map((image) => {
        return (
          <SwiperSlide key={image.imagesIdx}>
            <ImageStyle src={`https://aeoragy.com/api/file/images/${image.imagesName}`} />
          </SwiperSlide>
        );
    });

  return (
    <HomePageStyle>
      <InfoTextBox>
        <TitleTextStyle>{titleText}</TitleTextStyle>
        <DetailTextStyle>{detailText}</DetailTextStyle>
        
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
        {slideImages}

      </Swiper>
      
    </HomePageStyle>
  );
}

export default Home;

const HomePageStyle = styled.div`
  width: 100%;
  height: 100%;
`;

const InfoTextBox = styled.div`
  width: 25vw;
  height: 40vh;
  top: 50%;
  left: 20%;
  transform: translate(-50%, -50%);
  z-index: 3;
  position: absolute;
  background: rgba(0, 0, 0, 0.6);
  color: white;
  overflow: auto;
  &::-webkit-scrollbar {
    display: none;
  }
`;

const TitleTextStyle = styled.div`
  font-size: 3vw;
  font-family: gyeonggi_title_medium;
  margin-top: 25px;
  margin-bottom: 10px;
  padding-left: 10px;
`;

const DetailTextStyle = styled.div`
  font-size: 1.5vw;
  font-family: gyeonggi_title_v_bold;
  padding-left: 10px;
  white-space: pre-wrap;
  line-height: 1.4;
`

const ImageStyle = styled.img`
  display: block;
  width: 100%;
  height: 100%;
  object-fit: cover;
`;
