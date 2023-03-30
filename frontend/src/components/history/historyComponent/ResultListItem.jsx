import styled from 'styled-components';

const ResultListItem = (props) => {
    
    return (
        <Item>
            <div>{props.idx}</div>
            <div>{props.gameRoomIdx}</div>
            <div>{props.resultRank}위</div>
            <div>{props.resultKill}명</div>
            <div>{props.mapIdx} 번맵</div>
        </Item>
    );
};

export default ResultListItem;

const Item = styled.div`
    width: 100%;
    height: 3rem;
    margin: 3px 3px 3px 3px;
    display: flex;
    align-items: center;
    justify-content: space-between;
`;
