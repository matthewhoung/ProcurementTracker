CREATE TABLE vplus_dev.forms_receiveform (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id INT,
    status ENUM('pending', 'inprogress', 'finished'),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (form_id) REFERENCES forms(id)
);
