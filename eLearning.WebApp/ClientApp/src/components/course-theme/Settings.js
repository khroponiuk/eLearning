import React, { useState } from 'react';
import api from '../../utils/api'
import { RadioFieldset } from './RadioFieldset';
import { FileUploadControl } from './FileUploadControl';

export function Settings(props) {
  const [isLectureEnabled, setIsLectureEnabled] = useState(props.isLectureEnabled);
  const [isLabEnabled, setIsLabEnabled] = useState(props.isLabEnabled);
  const [isQuizEnabled, setIsQuizEnabled] = useState(props.isQuizEnabled);
  const [quizId, setQuizId] = useState(props.quizId);

  function handleSaveClick(e) {
    async function save() {
      const formData = new FormData();
      const lectureFileInput = document.querySelector('input[name="lectureFile"]');
      const labFileInput = document.querySelector('input[name="labFile"]');

      formData.append('id', props.themeId);
      formData.append('isLectureEnabled', isLectureEnabled);
      formData.append('isLabEnabled', isLabEnabled);
      formData.append('isQuizEnabled', isQuizEnabled);
      formData.append('externalQuizId', quizId);
      formData.append('lectureFile', lectureFileInput.files[0]);
      formData.append('labFile', labFileInput.files[0]);

      const response = await api.form('api/theme/configure', formData);
      debugger
      console.log(response)
      props.setThemeState(response);
    }
    save();
  }

  return (
    <div>
      <RadioFieldset name="isLectureEnabled" title="Lecture" value={isLectureEnabled} handleOptionChange={(value) => setIsLectureEnabled(value)} />
      <RadioFieldset name="isLabEnabled" title="Lab" value={isLabEnabled} handleOptionChange={(value) => setIsLabEnabled(value)}/>
      <RadioFieldset name="isQuizEnabled" title="Quiz" value={isQuizEnabled} handleOptionChange={(value) => setIsQuizEnabled(value)}/>

      <div className="form-group row">
        <label htmlFor="quizId" className="col-sm-2 col-form-label">External Quiz Id</label>
        <div className="col-sm-10">
          <input type="text" className="form-control" id="quizId" placeholder="External Quiz ID" value={quizId} onChange={(e) => setQuizId(e.target.value)}/>
        </div>
      </div>

      <FileUploadControl name="lectureFile" title="Lecture file" placeholder={props.lectureFilePath}/>
      <FileUploadControl name="labFile" title="Lab file" placeholder={props.labFilePath}/>

      <div className="row">
        <div className="col text-center">
          <button className="btn btn-secondary theme-save-btn" onClick={handleSaveClick}>Save</button>
        </div>
      </div>

    </div>
  );
}