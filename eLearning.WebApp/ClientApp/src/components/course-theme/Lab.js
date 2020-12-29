import React from 'react';
import { FileViewer } from './FileViewer';

export function Lab(props) {
  
  return (
    <FileViewer path={props.filePath} />
  );
}