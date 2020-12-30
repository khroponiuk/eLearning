import React from 'react';

export function Quiz(props) {
  
  return (
    <div className='iframe-holder'>
      <iframe
        name='proprofs'
        id='proprofs'
        width='100%'
        height='700'
        frameBorder='0'
        marginWidth='0'
        marginHeight='0'
        src={`https://www.proprofs.com/quiz-school/story.php?title=${props.id}&id=3005002&ew=630`}
      ></iframe>
      <div style={{ fontSize: '10px', fontFamily: 'Arial, Helvetica, sans-serif', color: '#000', textAlign: 'left' }}>
        <a href={'https://www.proprofs.com/quiz-school/story.php?title=' + props.id} target='_blank' title='ProProfs - Untitled Quiz'>ProProfs - Untitled Quiz</a>
      </div>
    </div>
  );
}