create table book
(
    id             int auto_increment
        primary key,
    name           varchar(45) not null,
    author         varchar(45) not null,
    publisher      varchar(45) not null,
    quantity       int         not null,
    price          int         not null,
    published_date varchar(45) not null,
    isbn           varchar(45) not null,
    description    varchar(45) not null
);

INSERT INTO woojin_library.book (id, name, author, publisher, quantity, price, published_date, isbn, description) VALUES (1, '세이노의 가르침', '세이노', '데이윈', 7, 7200, '2023-03-02', '979116847S 9791168473690', '베스트 셀러입니다.');
INSERT INTO woojin_library.book (id, name, author, publisher, quantity, price, published_date, isbn, description) VALUES (2, '내일을 바꾸는 인생 공부', '신진상', '미디어 숲', 5, 17800, '2023-05-10', '979115874N 9791158741884', '예약 판매중입니다.');
INSERT INTO woojin_library.book (id, name, author, publisher, quantity, price, published_date, isbn, description) VALUES (3, '메리골드 마음 세탁소', '윤정은', '북로망스', 15, 13500, '2023-03-06', '979119189M 9791191891287', '마음을 세탁하세요.');
INSERT INTO woojin_library.book (id, name, author, publisher, quantity, price, published_date, isbn, description) VALUES (4, '돌연한 출발', '프란츠 카프카', '민음사', 15, 16000, '2023-04-07', '978893742D 9788937427831', '갑작스러운 출발.');
