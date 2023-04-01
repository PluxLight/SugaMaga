import styled from 'styled-components';

import img03 from './../../../image/img03.png'
import keyboardImg from './../../../image/keyboard.png'

const Manual = () => {

    return (
        <ManualStyle>
            <TitleTextStyle>조작법</TitleTextStyle>
            <ImageStyle src={ img03 }></ImageStyle>
            <ImageStyle src={ keyboardImg }></ImageStyle>
        </ManualStyle>
    );
};

export default Manual;

const ManualStyle = styled.div`
    width: 100%;
    height: 80%;
    overflow: auto;
    &::-webkit-scrollbar {
    display: none;
    }
`

const TitleTextStyle = styled.div`
    font-size: 32px;
    font-family: gyeonggi_title_medium;
    margin-bottom: 15px;
`;

const ImageStyle = styled.img`
    display: block;
    width: 100%;
    height: 50%;
    object-fit: fill;
    margin-top: 30px;
`;
