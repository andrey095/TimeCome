import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div id="c3dsrw">
        <NavMenu />
        <Container fluid  className="main">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
