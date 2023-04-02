import React from "react";

import styled from 'styled-components';

const ButtonMaker = ({ text }) => {

    return (
        <ButtonStyle>
            {text}
        </ButtonStyle>
    );
};

export default ButtonMaker;

const ButtonStyle = styled.button`
    display: flex;
    float: right;

    width: 230px;
    height: 45px;
    margin-top: 5%;
    align-items: center;
    justify-content: center;
    font-size: 22px;
    font-family: gyeonggi_title_bold;

    border-radius: 5px;
    padding: 10px 25px;
    font-weight: 500;
    background: transparent;
    cursor: pointer;
    transition: all 0.3s ease;
    position: relative;
    box-shadow:inset 2px 2px 2px 0px rgba(255,255,255,.5),
    7px 7px 20px 0px rgba(0,0,0,.1),
    4px 4px 5px 0px rgba(0,0,0,.1);
    outline: none;

    background-color: DeepPink;
    background-image: linear-gradient(315deg, DeepPink 0%, pink 74%);
    border: none;
    z-index: 1;
    
    &:after {
      position: absolute;
      content: "";
      width: 100%;
      height: 0;
      bottom: 0;
      left: 0;
      z-index: -1;
      border-radius: 5px;
      background-color: HotPink;
      background-image: linear-gradient(315deg, HotPink 0%, LightPink 74%);
      box-shadow:
      -7px -7px 20px 0px #fff9,
      -4px -4px 5px 0px #fff9,
      7px 7px 20px 0px #0002,
      4px 4px 5px 0px #0001;
      transition: all 0.3s ease;
    }

    &:hover {
      color: #fff;
    }

    &:hover:after {
      top: 0;
      height: 100%;
    }
    
    &.active {
      top: 2px;
    }
`;