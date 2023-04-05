import styled from 'styled-components';

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import required modules
import { Autoplay, Pagination, Navigation } from 'swiper';

import logo from './../../../image/logo.png'
import concept from './../../../image/concept.jpeg'
import snow from './../../../image/snow2.png'
import bad from './../../../image/badlands.png'

const Artwork = () => {
    const images = [logo, concept, snow, bad];

    const slideImages = images.map((image) => {
        return (
            <SwiperSlide>
                <ImageStyle src={image} />
            </SwiperSlide>
        );
    });

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
                style={{ width: '100%', height: '100%',  position: 'relative', margin: '0 auto' }}
            >
                {slideImages}

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