import React, { useState } from 'react';

export function FileUploadControl(props) {
  var [placeholder, setPaceholder] = useState(props.placeholder || "Choose file");

  function handleFileInputChange(e) {
    const value = e.target.value.split("\\").pop();
    setPaceholder(value);
  }
  return (
    <div className="form-group row">
      <label htmlFor={props.name} className="col-sm-2 col-form-label">{props.title}</label>

      <div className="col-sm-10">
        <div className="custom-file">
          <input type="file" className="custom-file-input" name={props.name} id={props.name} onChange={handleFileInputChange} />
          <label className="custom-file-label" htmlFor="name">{placeholder}</label>
        </div>
      </div>
    </div>
  );
}