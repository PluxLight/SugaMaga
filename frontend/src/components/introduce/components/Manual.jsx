import styled from 'styled-components';

import img03 from './../../../image/img03.png'
import keyboardImg from './../../../image/Keyboard_for_Manual_small.png'

const Manual = () => {
    const tableHeader = [
        { key: 0, render: '기능' },
        { key: 1, render: '단축키' },
        { key: 2, render: '기능' },
        { key: 3, render: '단축키' },
    ];
    const tableRows = [
        {
            id: '1',
            columns: [
                {
                    key: 0,
                    render: '캐릭터 조작'
                },
                {
                    key: 1,
                    render: 'W, A, S, D'
                },
                {
                    key: 2,
                    render: '아이템 습득'
                },
                {
                    key: 3,
                    render: 'E'
                },
            ]
        },
        {
            id: '2',
            columns: [
                {
                    key: 0,
                    render: '공격'
                },
                {
                    key: 1,
                    render: '마우스 좌클릭'
                },
                {
                    key: 2,
                    render: '스킬 사용'
                },
                {
                    key: 3,
                    render: '마우스 우클릭'
                },
            ]
        },
        {
            id: '3',
            columns: [
                {
                    key: 0,
                    render: '점프'
                },
                {
                    key: 1,
                    render: '스페이스 바'
                },
                {
                    key: 2,
                    render: '아이템 버리기'
                },
                {
                    key: 3,
                    render: 'A'
                },
            ]
        },
        {
            id: '4',
            columns: [
                {
                    key: 0,
                    render: '임시 값'
                },
                {
                    key: 1,
                    render: '임시 값'
                },
                {
                    key: 2,
                    render: '임시 값'
                },
                {
                    key: 3,
                    render: '임시 값'
                },
            ]
        },
        {
            id: '5',
            columns: [
                {
                    key: 0,
                    render: '임시 값'
                },
                {
                    key: 1,
                    render: '임시 값'
                },
                {
                    key: 2,
                    render: '임시 값'
                },
                {
                    key: 3,
                    render: '임시 값'
                },
            ]
        },
    ]

    return (
        <ManualStyle>
            <TitleTextStyle>조작법</TitleTextStyle>
            <ImageStyle src={ img03 }></ImageStyle>
            <ImageStyle src={keyboardImg}></ImageStyle>
            <TableStyle>
                <thead>
                    <tr>
                        {tableHeader.map(header => (
                            <ThStyle key={header.key}>{header.render}</ThStyle>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {tableRows.map(cols => (
                        <tr key={cols.id}>
                            {cols.columns.map(col => (
                                <TdDetailStyle key={col.key}>{col.render}</TdDetailStyle>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </TableStyle>
        </ManualStyle>
    );
};

export default Manual;

const ManualStyle = styled.div`
    width: 100%;
    height: 90%;
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

const TableStyle = styled.table`
    margin: 30px auto;
`

const ThStyle = styled.th`
    font-size: 34px;
    font-family: gyeonggi_title_bold;
    text-align: center;
`

const TdDetailStyle = styled.td`
    font-size: 22px;
    font-family: gyeonggi_title_medium;
    text-align: center;
`