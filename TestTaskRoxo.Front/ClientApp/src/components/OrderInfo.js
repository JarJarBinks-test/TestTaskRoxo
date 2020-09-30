import React, { Component } from 'react';
import { Spinner, Table, Container, Row, Col, Card, CardBody } from 'reactstrap';

export class OrderInfo extends Component {

  constructor(props) {
    super(props);
    this.state = {};
  }

  componentDidMount() {
  }

  static renderOrderInfo(order, statuses, total) {
    var date = new Date(order.dateCreated);
    var status = statuses.filter(function (a) { return a.orderStatusId == order.orderStatusId; });
    return (
      <Table striped className='table order-info table-striped'>
        <tbody>
          <tr>
            <td>Number#</td>
            <td>{order.orderId}</td>
          </tr>
          <tr>
            <td>Date</td>
            <td>{date.toLocaleDateString()} {date.toLocaleTimeString()}</td>
          </tr>
          <tr className="with-status-color">
            <td>Status</td>
            <td><div os={order.orderStatusId}></div> {(status.length > 0 ? status[0].name : null)}</td>
          </tr>
          <tr>
            <td>Total</td>
            <td>{(!!total ? total.toFixed(2) : null)}</td>
          </tr>
        </tbody>
      </Table>
    );
  }

  render() {
    return (
      <div className="order-info-wrapper">
        {OrderInfo.renderOrderInfo(this.props.order, this.props.statuses, this.props.total)}
      </div>
    );
  }
}
