import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as MontyHallSimulatorStore from '../store/MontyHallSimulator';

// At runtime, Redux will merge together...
type MontyHallSimulatorProps =
  MontyHallSimulatorStore.PlayerState // ... state we've requested from the Redux store
  & typeof MontyHallSimulatorStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ personalId: string }>; // ... plus incoming routing parameters


class FetchData extends React.PureComponent<MontyHallSimulatorProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">Player Access Information</h1>
        <p>This component demonstrates fetching data from the authserver for user access</p>
        {this.renderPlayerAccessTable()}
        {this.props.isLoading && <p>Loading...</p>}
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
      //const personalId = parseInt(this.props.match.params.personalId, 14) || 0;
      this.props.requestPlayerAccessToken(198005064772);
  }

  private renderPlayerAccessTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Personal ID</th>
            <th>Expiray</th>
            <th>Access Token</th>
          </tr>
        </thead>
        <tbody>
            <tr key={this.props.personalId}>
                <td>{this.props.personalId}</td>
                <td>{this.props.expiry}</td>
                <td>{this.props.accessToken}</td>
            </tr>
        </tbody>
      </table>
    );
  }
}

export default connect(
    (state: ApplicationState) => state.playerState, // Selects which state properties are merged into the component's props
  MontyHallSimulatorStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchData as any);
