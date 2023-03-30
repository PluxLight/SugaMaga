import styled from 'styled-components';
import ImageSlider, { Slide } from "react-auto-image-slider";

import React, { useState } from "react";

import img01 from './../../image/img01.png'
import img02 from './../../image/img02.jpeg'
import img03 from './../../image/img03.png'
import img04 from './../../image/img04.jpg'
import img05 from './../../image/img05.png'

function Home() {

  return (
    <HomePageStyle>
      <ImageSlider effectDelay={500} autoPlayDelay={2000}>
        <Slide>
          <img alt="img1" src={img01} />
        </Slide>
        <Slide>
          <img alt="img2" src={img02} />
        </Slide>
        <Slide>
          <img alt="img3" src={img03} />
        </Slide>
        <Slide>
          <img alt="img4" src={img04} />
        </Slide>
        <Slide>
          <img alt="img5" src={img05} />
        </Slide>
      </ImageSlider>
    </HomePageStyle>
  );
}

export default Home;


const HomePageStyle = styled.div`
  width: 100%;
  height: 10%;
`;