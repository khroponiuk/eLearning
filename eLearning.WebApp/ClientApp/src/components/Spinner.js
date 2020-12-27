import React from 'react';
import Loader from 'react-loader-spinner';

export default function Spinner() {

  return (
    <div className="spinner-overlay">
      <Loader type="Bars" color="#00BFFF" height={60} width={60} />
    </div>
  );
}
