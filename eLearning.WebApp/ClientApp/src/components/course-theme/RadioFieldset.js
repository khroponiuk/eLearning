import React from 'react';

export function RadioFieldset(props) {

  function onChangeHandler(e) {
    props.handleOptionChange(e.target.value === "true");
  }

  return (
    <fieldset className="form-group">
      <div className="row">
        <legend className="col-form-label col-sm-2 pt-0">{props.title}</legend>
        <div className="col-sm-10">
          <div className="form-check form-check-inline">
            <input className="form-check-input" type="radio" name={props.name} id={`${props.name}_Enabled`} value={true} checked={props.value == true} onChange={onChangeHandler} />
            <label className="form-check-label" htmlFor={`${props.name}_Enabled`}>
              Enabled
            </label>
          </div>
          <div className="form-check form-check-inline">
            <input className="form-check-input" type="radio" name={props.name} id={`${props.name}_Disabled`} value={false} checked={props.value == false} onChange={onChangeHandler} />
            <label className="form-check-label" htmlFor={`${props.name}_Disabled`}>
              Disabled
            </label>
          </div>
        </div>
      </div>
    </fieldset>
  );
}