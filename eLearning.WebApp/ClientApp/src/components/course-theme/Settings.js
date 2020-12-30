import React, { useState } from 'react';
import api from '../../utils/api'
import { RadioFieldset } from './RadioFieldset';
import { FileUploadControl } from './FileUploadControl';

export function Settings(props) {
  const [isLectureEnabled, setIsLectureEnabled] = useState(props.isLectureEnabled || true)
  const [isLabEnabled, setIsLabEnabled] = useState(props.isLabEnabled || true)
  const [isQuizEnabled, setIsQuizEnabled] = useState(props.isQuizEnabled || true)

  function handleSaveClick(e) {
    async function save() {
      const formData = new FormData();
      const lectureFileInput = document.querySelector('input[name="lectureFile"]');
      const labFileInput = document.querySelector('input[name="labFile"]');

      formData.append('id', props.themeId);
      formData.append('isLectureEnabled', isLectureEnabled);
      formData.append('isLabEnabled', isLabEnabled);
      formData.append('isQuizEnabled', isQuizEnabled);
      formData.append('lectureFile', lectureFileInput.files[0]);
      formData.append('labFile', labFileInput.files[0]);

      await api.form('api/theme/configure', formData);
    }
    save();
  }

  return (
    <div>
      <RadioFieldset name="isLectureEnabled" title="Lecture" value={isLectureEnabled} handleOptionChange={(value) => setIsLectureEnabled(value)} />
      <RadioFieldset name="isLabEnabled" title="Lab" value={isLabEnabled} handleOptionChange={(value) => setIsLabEnabled(value)}/>
      <RadioFieldset name="isQuizEnabled" title="Quiz" value={isQuizEnabled} handleOptionChange={(value) => setIsQuizEnabled(value)}/>

      <FileUploadControl name="lectureFile" title="Lecture file" placeholder={props.lectureFile}/>
      <FileUploadControl name="labFile" title="Lab file" placeholder={props.labFile}/>

      <div className="row">
        <div className="col text-center">
          <button className="btn btn-secondary theme-save-btn" onClick={handleSaveClick}>Save</button>
        </div>
      </div>

    </div>
  );
}