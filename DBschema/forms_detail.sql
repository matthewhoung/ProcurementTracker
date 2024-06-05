create table vplus_dev.forms_detail(
	detail_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    detail_name varchar(255),
    detail_description text,
    quantity int,
    unit_price int,
    unit_id int,
    detail_total int,
    is_check tinyint(1),
    FOREIGN KEY (form_id) REFERENCES forms(id)
);