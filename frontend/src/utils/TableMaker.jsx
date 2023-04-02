import React from "react";

import styled from 'styled-components';

const TableMaker = ({ headers, rows }) => {

    return (
        <TableStyle>
            <thead>
                <tr>
                    {headers.map(header => (
                        <ThStyle key={header}>{header}</ThStyle>
                    ))}
                </tr>
            </thead>
            <tbody>
                {rows.map(cols => (
                    <tr key={cols.id}>
                        {cols.columns.map(col => (
                            (col.key === 0 ?
                                <TdHeadStyle key={col.key}>{col.render}</TdHeadStyle>
                                : <TdDetailStyle key={col.key}>{col.render}</TdDetailStyle>)
                        ))}
                    </tr>
                ))}
            </tbody>
        </TableStyle>
    );
};

export default TableMaker;

const TableStyle = styled.table`
    width: 60%;
    margin: 30px auto;
    border: 3px solid;
    border-radius: 16px;
    border-color: pink;
    background-color: pink;
`

const ThStyle = styled.th`
    height: 50px;
    font-size: 34px;
    font-family: gyeonggi_title_bold;
    text-align: center;
`

const TdHeadStyle = styled.td`
    font-size: 28px;
    font-family: gyeonggi_title_bold;
    text-align: center;
`

const TdDetailStyle = styled.td`
    font-size: 22px;
    font-family: gyeonggi_title_medium;
    text-align: center;
    background-color: white;
`