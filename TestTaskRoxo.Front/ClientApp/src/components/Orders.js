import React, { Component } from 'react';
import { Spinner, Table, Container, Row, Col, Card, CardBody } from 'reactstrap';
import { OrderDetails } from './OrderDetails';
import { OrderStatusesLegend } from './OrderStatusesLegend';
import { OrderInfo } from './OrderInfo';

export class Orders extends Component {

  constructor(props) {
    super(props);
    this.state = {
      orders: [],
      loading: true,
      selectedOrder: null,
      statuses: [],
      total: null
    };
    this.selectOrder = this.selectOrder.bind(this);
    this.updateStatuses = this.updateStatuses.bind(this);
    this.updateTotal = this.updateTotal.bind(this);
  }

  componentDidMount() {
    this.populateOrdersData();
  }

  selectOrder(order) {
    this.setState({ selectedOrder: order });    
  }

  static renderOrders(orders) {
    return (
      <Table striped className='table table-striped with-status-color' >
        <tbody>
          {orders.map(order => {
            var date = new Date(order.dateCreated);
            return (<tr selected={(!!this.state.selectedOrder ? true : false)} key={order.orderId} onClick={(function () { this.selectOrder(order); }).bind(this)}>
              <td><div os={order.orderStatusId}></div> Order# {order.orderId}</td>
              <td>{date.toLocaleDateString()} {date.toLocaleTimeString()}</td>
            </tr>);
          })}
        </tbody>
      </Table>
    );
  }

  updateStatuses(statuses) {
    this.setState({ statuses: statuses });    
  }

  updateTotal(total) {
    this.setState({ total: total });    
  }

  render() {
    let ordersContents = this.state.loading
      ? <Spinner color="primary" />
      : Orders.renderOrders.bind(this)(this.state.orders);
    
    return (
      <div className="order-wrapper">
        <Container>
          <Row>
            <Col xs="4">
              <Card className="left-panel">
                <CardBody>
                  <Container fluid="true">
                    <Row>
                      <Col>
                        {ordersContents}                      
                      </Col>
                    </Row>
                    <Row>
                      <Col>
                        <OrderStatusesLegend backportStatuses={this.updateStatuses}></OrderStatusesLegend>
                      </Col>
                    </Row>
                  </Container>
                </CardBody>
              </Card>
            </Col>
            <Col xs="8">
              <Container fluid="true" className="right-container">
                <Row>
                  {!!this.state.selectedOrder ? (<Card className="right-panel"><CardBody><OrderInfo order={this.state.selectedOrder} statuses={this.state.statuses} total={this.state.total}></OrderInfo></CardBody></Card>) : null}                  
                </Row>
              </Container>
              <Container fluid="true">
                <Row>
                  {!!this.state.selectedOrder ? (<Card className="right-panel"><CardBody><OrderDetails orderId={this.state.selectedOrder.orderId} backportTotal={this.updateTotal}></OrderDetails></CardBody></Card>) : null}
                </Row>
              </Container>
            </Col>
          </Row>
        </Container>
        
        
      </div>
    );
  }

  async populateOrdersData() {
    var response = await fetch('order/');
    var data = await response.json();
    this.setState({ orders: data, loading: false });
  }
}
