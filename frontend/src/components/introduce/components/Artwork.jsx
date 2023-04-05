import styled from 'styled-components';
import React, { useEffect, useState } from "react";

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import required modules
import { Autoplay, Pagination, Navigation } from 'swiper';

import { getImageList } from '../../../api/file'

const Artwork = () => {
    const [images, setImages] = useState([]);
    
    const param = "artwork";
        
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