import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { createHashHistory } from 'history'

export const historyr = createHashHistory()

export class Post extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            lat: this.props.location.state.latProps,
            lon: this.props.location.state.lonProps,
            place: this.props.location.state.placeProps,
            yr: this.props.location.state.yrProps,
            days: this.props.location.state.daysProps,
        };

    

        
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps.location.state !== this.props.location.state) {
            this.setState({
                place: this.props.location.state.placeProps,
                 lat: this.props.location.state.latProps,
                lon: this.props.location.state.lonProps,
                yr: this.props.location.state.yrProps,
       
            })
        }

     
    }


    render() {

        let yrList = this.state.yr.map((Tag, index) => {
            return (
                <table className='test2'>
                    <td className='test7' ><b> {Tag.temperature}</b></td>
                    <td className='test6'> {Tag.time}</td>
            

                </table>

            );
        });

           let daysList = this.state.days.map((Tag, index) => {
            return (
                <div>
                   <b> {Tag.day}</b>

                </div>

            );
        });

        /*let diffList = this.state.yr.map((Tag, index) => {
            return (
                <div>
                   <b> {Tag.temperature}</b>

                </div>

            );
        });


        let smhiList = this.state.smhi.map((Tag, index) => {
            return (
                <table className='test4'>
                    <td className='test7' ><b> {Tag.temperature}</b></td>
                    <td className='test6'> {Tag.time}</td>
                </table>

            );
        });*/

       
        return (
            <form>
                <h1>{this.state.place}</h1>
                <table className='test2'>
                    <tr  className='test3'>
                        <td className='firstColumn'><div className='test'></div></td>
                        <td className='secondColumn'></td>
                        <td className='thirdColumn'><div className='testSMHI'></div></td>
                    </tr>
                    <td className='firstColumn'>{yrList}</td>
                    <td className='secondColumn'></td>
                    <td className='thirdColumn'>{daysList}</td>
                  
                    
                   
                </table>

             
            </form>
           
        );
    }
}

//export default withRouter(Post);