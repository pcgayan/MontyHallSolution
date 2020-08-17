import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Monty Hall Game Simulator</h1>
        <h3>HOW TO Steps</h3>
        <p>1. Check out Readme.MD on 'https://github.com/pcgayan/MontyHallSolution'</p>
        <p>2. Once it is loaded with "Grant Player Access" where authenticated for a demo user (hardcoded on client) with JWT token click on "Simulate" link</p>
        <p>3. Click on "Simulate" button.</p>

        <h3>Limitations</h3>
        <p>1. Add number of simulations and switch door is not accepted as user params from Client web. Instead you can edit https://github.com/pcgayan/MontyHallSolution/blob/master/ClientWebApplication/ClientApp/src/components/FetchMontyHallSimulatorData.tsx 
line 44, 45 to add thos</p>
        <p>2. After trigger hit of Simulations button you have to refesh the page simulate again</p>

  </div>
);

export default connect()(Home);
