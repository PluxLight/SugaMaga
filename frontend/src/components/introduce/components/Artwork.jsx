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

const Artwork = () => {

    return (
        <ArtworkStyle>
            <TitleTextStyle>아트워크</TitleTextStyle>
            <Swiper
                modules={[Autoplay, Pagination, Navigation]}
                spaceBetween={30}
                centeredSlides={true}
                autoplay={{
                delay: 2000,
                disableOnInteraction: false,
                }}
                className="mySwiper"
                style={{ width: '100%', height: '100%',  position: 'relative', border: '0.5px solid #aaa' }}
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
        </ArtworkStyle>
    );
};

export default Artwork;

const ArtworkStyle = styled.div`
`

const ImageStyle = styled.img`
    display: block;
    width: 100%;
    height: 100%;
    object-fit: cover;
`;


const TitleTextStyle = styled.div`
    font-size: 32px;
    font-family: gyeonggi_title_medium;
    margin-top: 25px;
    margin-bottom: 15px;
`;