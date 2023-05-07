create table borrowed_book
(
    borrowed_id   int auto_increment
        primary key,
    user_id       varchar(45) not null,
    book_id       int         not null,
    borrowed_date varchar(45) not null
);

INSERT INTO woojin_library.borrowed_book (borrowed_id, user_id, book_id, borrowed_date) VALUES (1, 'userid12', 1, '2001-09-11');
INSERT INTO woojin_library.borrowed_book (borrowed_id, user_id, book_id, borrowed_date) VALUES (2, 'userid12', 2, '2001-09-11');
