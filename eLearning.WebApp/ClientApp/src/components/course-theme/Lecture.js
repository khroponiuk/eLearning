import React from 'react';
import { FileViewer } from './FileViewer';

export function Lecture(props) {
  
  return (
    <FileViewer path={props.filePath} />
  );
}