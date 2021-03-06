import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as PlayerStore from '../store/Player';
import * as MontyHallSimulatorStore from '../store/MontyHallSimulator';

type MontyHallSimulatorStoreProps =
    MontyHallSimulatorStore.MontyHallSimulatorState // ... state we've requested from the Redux store
    & typeof MontyHallSimulatorStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ numberOfSimulations: string; switchDoor: string; currentSimulationAtIndex: string; }>; // ... plus incoming routing parameters


class MontyHallSimulatorData extends React.PureComponent<MontyHallSimulatorStoreProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    //this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
            <h1 id="tabelLabel">Monty Hall Game Simulator</h1>
            <p>This component Simulate Monty Hall Game </p>
            <button type="button" className="btn btn-primary btn-lg"
                onClick={() => { this.simulationDataFetch(); }}> Simulate
            </button>
            {this.renderPlayerAccessTable()}
            {this.props.isLoading && <p>Loading...</p>}
            {this.props.error.length != 0 && <p>Server responded with error : {this.props.error}</p>}
      </React.Fragment>
    );
    }

    private simulationDataFetch() {
        const accessToekn = localStorage.getItem('accessToken');
        //console.log("retriving saved accessToekn : " + accessToekn

        const numberOfSimulations = 8;
        const switchDoor = false;

        console.log("NumberOfSimulations : " + numberOfSimulations + " with switchDoor " + switchDoor);
        this.props.requestMontyHallSimulatorAction(numberOfSimulations, switchDoor, accessToekn == null ? '' : accessToekn);
    }

    private ensureDataFetched() {
        //console.log("numberOfSimulations : " + numberOfSimulations + " with switchDoor " + switchDoor);
        if (this.props.games.length == this.props.numberOfSimulations) {
            return;
        }

        this.props.requestMontyHallSimulatorAction(this.props.numberOfSimulations, this.props.switchDoor, this.props.accessToekn == null ? '' : this.props.accessToekn);
  }

  private renderPlayerAccessTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Game Number</th>
            <th>Stage</th>
            <th>Message</th>
            <th>State</th>
          </tr>
            </thead>
            <tbody>
            {this.props.games.map((game: MontyHallSimulatorStore.GameState) =>
                <tr key={game.id}>
                    <td>{game.id + 1}</td>
                    <td>{game.stage}</td>
                    <td>{game.message}</td>
                    <td>{game.status}</td>
                </tr>
            )}
            </tbody>
       </table>
    );
  }
}

export default connect(
    (state: ApplicationState) => state.montyHallSimulatorState,// Selects which state properties are merged into the component's props
    MontyHallSimulatorStore.actionCreators // Selects which action creators are merged into the component's props
)(MontyHallSimulatorData as any);
