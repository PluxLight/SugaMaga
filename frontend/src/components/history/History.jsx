import styled from 'styled-components';
import React, { useEffect, useState } from "react";

import { useRecoilState } from 'recoil';
import { user, nickname } from "../../Store";

import PageHeader from "../../utils/PageHeader";
import { getHistory } from "../../api/sign";
import ResultListItem from "./historyComponent/ResultListItem";

const History = () => {
    const [recoilUser, setRecoilUser] = useRecoilState(user);
    const [recoilNickname, setRecoilNickname] = useRecoilState(nickname);
    const [resultList, setResutList] = useState([]);

    useEffect(() => {
        let config = {
            headers: {
                uid: recoilUser.uid,
            }
        };

        getHistory(config,
            ({ data }) => {
                // console.log(data.result);
                setResutList(data.result);
            },
            (error) => {
                console.log(error);
            });
        
    }, []);

    const resultListUl = resultList.map((resultItem, index) => {
        return (
            <ResultListItem
                key={resultItem.userGameResultIdx}
                idx={resultItem.userGameResultIdx}
                gameRoomIdx={resultItem.gameRoomIdx}
                resultRank={resultItem.resultRank}
                resultKill={resultItem.resultKill}
                mapIdx={resultItem.mapIdx}
            />
        );
      });

    return (
        <HistoryStyle>
            <PageHeader title="게임기록" horizonTitle="History" />
            <TableHeader>
                <h2>순번</h2>
                <h2>게임 번호</h2>
                <h2>게임 순위</h2>
                <h2>처치 수</h2>
                <h2>맵</h2>
            </TableHeader>
            <ul>{resultListUl}</ul>
        </HistoryStyle>
    );
};

export default History;

const HistoryStyle = styled.div`    
    width: 70%;
    height: 100%;
    justify-content: center;
    background-color: white;
    padding: 1%;
`;

const TableHeader = styled.div`
    width: 100%;
    height: 3rem;
    margin: 3px 3px 3px 3px;
    display: flex;
    align-items: center;
    justify-content: space-between;
`;