import React from 'react';

export function FileViewer(props) {
  return (
    <div>
      {
        props.path ? 
          <object data={props.path} type="application/pdf" width="100%" height="750">
            <a href={props.path}>Download</a>
          </object>
          : null
      }
    </div>
  );
}