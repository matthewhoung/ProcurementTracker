create table vplus_dev.forms_affiliate(
	aff_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    form_affiliate_id int,
    FOREIGN KEY (form_id) REFERENCES forms(id),
    FOREIGN KEY (form_affiliate_id) REFERENCES forms(id)
);