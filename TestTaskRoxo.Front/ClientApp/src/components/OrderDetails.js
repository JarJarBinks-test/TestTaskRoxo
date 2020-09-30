import React, { Component } from 'react';
import { Spinner, Table, Container, Row, Col, Card, CardBody } from 'reactstrap';

export class OrderDetails extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ordersDetails: [],
      loading: true
    };
  }

  componentDidMount() {
    this.populateOrderDetailsData(this.props.orderId);
  }

  componentDidUpdate(prevProps) {
    if (prevProps.orderId !== this.props.orderId) {
      this.populateOrderDetailsData(this.props.orderId);
    }
  }

  static renderOrderDetails(ordersDetails) {
    var totalQty = 0;
    var totalAmt = 0;
    for (var i = 0; i < ordersDetails.length; i++) {
      totalAmt += ordersDetails[i].amount;
      totalQty += ordersDetails[i].quantity;
    }
    return (
      <Table striped className='table table-striped'>
        <thead>
          <tr>
            <th>Product Name</th>
            <th>Qty</th>
            <th>Price</th>
            <th>Total</th>
          </tr>          
        </thead>
        <tbody>
          {ordersDetails.map(od => { 
            return (<tr key={od.orderDetailId}>
              <td>Product# {od.productId}</td>
              <td>{od.quantity}</td>
              <td>{od.price}</td>
              <td>{od.amount}</td>
            </tr>);
          })}
          <tr><td colSpan={4}>&nbsp;</td></tr>
          <tr>
            <td>Total</td>
            <td>{(!!totalQty ? totalQty.toFixed(2) : null)}</td>
            <td></td>
            <td>{(!!totalAmt ? totalAmt.toFixed(2) : null)}</td>
          </tr>
        </tbody>
      </Table>
    );
  }

  render() {
    let ordersDetailsContents = this.state.loading
      ? <Spinner color="primary" />
      : OrderDetails.renderOrderDetails(this.state.orderDetails);  

    return (
      <div className="order-details-wrapper">
        {ordersDetailsContents}
      </div>
    );
  }

  async populateOrderDetailsData(orderId) {
    var response = await fetch('orderDetail/' + orderId);
    var data = await response.json();
    var total = 0;
    for (var i = 0; i < data.length; i++) {
      total += data[i].amount;
    }
    this.props.backportTotal(total);
    this.setState({ orderDetails: data, loading: false });
  }
}
