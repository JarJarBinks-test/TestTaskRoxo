import React, { Component } from 'react';
import { Spinner, Table, Container, Row, Col, Card, CardBody } from 'reactstrap';

export class OrderStatusesLegend extends Component {

  constructor(props) {
    super(props);
    this.state = {
      orderStatuses: [],
      loading: true
    };
  }

  componentDidMount() {
    this.populateOrderStatusesData();
  }

  static renderOrdersStatuses(orderStatuses) {
    return (
      <Table className='table order-statuses with-status-color'>
        <tbody>
          {orderStatuses.map(os => {
            return (<tr key={os.orderStatusId}>
              <td><div os={os.orderStatusId}></div> {os.name}</td>
            </tr>);
          })}
        </tbody>
      </Table>
    );
  }

  render() {
    let ordersStatusesContents = this.state.loading
      ? <Spinner color="primary" />
      : OrderStatusesLegend.renderOrdersStatuses(this.state.orderStatuses);

    return (
      <div className="order-statuses-wrapper">
        {ordersStatusesContents}
      </div>
    );
  }

  async populateOrderStatusesData() {
    var response = await fetch('orderStatus/');
    var data = await response.json();
    this.props.backportStatuses(data);
    this.setState({ orderStatuses: data, loading: false });    
  }
}
