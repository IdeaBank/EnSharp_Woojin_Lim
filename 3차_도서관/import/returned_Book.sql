create table returned_Book
(
    returned_id   int auto_increment
        primary key,
    user_id       varchar(45) not null,
    book_id       int         not null,
    borrowed_date varchar(45) not null,
    returned_date varchar(45) not null
);

